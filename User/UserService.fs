namespace User

module Service =
        open Config
        open System
        open Hopac
        open Hopac.Infixes
        open User
        open Utils

        let fetchUser(user: string): Job<UserResponse> =
        
            let fetchUserInfo() =
                String.Format("{1}/{0}", user, BASE_URL)
                |> fetchJson<User> user
                
            let fetchOrganization (orgs: UserOrganization list) =
                let parseOrgs org = {
                    avatar=org.avatar_url
                    url=org.html_url
                }
                orgs
                |> List.map( fun {url=url} -> fetchJson<Organization> user url)
                |> Job.conCollect
                |> Job.map List.ofSeq
                |> Job.map(List.map parseOrgs)
    
            let fetchUserOrganization() =
                let url = String.Format("{1}/{0}/orgs", user, BASE_URL)
                fetchJson<UserOrganization list> user url
                |> Job.bind fetchOrganization
             
            let parseResponse (user,orgs) =
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

