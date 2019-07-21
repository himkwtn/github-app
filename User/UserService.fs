namespace User

module Service =
        open Config
        open System
        open Hopac
        open Hopac.Infixes
        open User
        open Utils

        let fetchUser (user : string) : Job<UserResponse> =

            let fetchUserInfo() =
                let url = String.Format("{0}/users/{1}", BASE_URL, user)
                fetchJson<User> user url

            let fetchOrganization (orgs : UserOrganization list) =
                let parseOrgs (org : Organization) = {
                    avatar = org.avatar_url
                    url = org.html_url
                }
                orgs
                |> List.map (fun { url = url } -> fetchJson<Organization> user url)
                |> Job.conCollect
                |> Job.map List.ofSeq
                |> Job.map (List.map parseOrgs)
            let fetchUserOrganization() =
                let url = String.Format("{0}/users/{1}/orgs", BASE_URL, user)
                fetchJson<UserOrganization list> user url
                |> Job.bind fetchOrganization

            let parseResponse (user : User, orgs) =
                 {
                     username = user.login
                     name = user.name
                     avatar = user.avatar_url
                     url = user.html_url
                     bio = user.bio
                     orgs = orgs
                 }

            fetchUserInfo() <*> fetchUserOrganization()
            |> Job.map parseResponse


        let searchUser (user : string) =

            let url = String.Format("{0}/search/users?q={1}+in:email+in:login+in:name", BASE_URL, user)

            let parseResponse ({ items = users } : SearchResult) : SearchResponse list =
                List.map
                    (fun (user : SearchUser) ->
                    {
                        username = user.login
                        avatar = user.avatar_url
                    }) users
            fetchJson<SearchResult> user url
            |> Job.map parseResponse
