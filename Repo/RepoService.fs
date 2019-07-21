namespace Repo

module Service =
    open Utils
    open Hopac
    open System
    open Model
    open Config

    let fetchRepo (user : string) =
        let url = String.Format("{0}/users/{1}/repos?sort=created&direction=desc&type=sources", BASE_URL, user)
        let parseRepo (repo : Repo) =
            {
                RepoResponse.name = repo.name
                url = repo.html_url
                description = repo.description
                language = repo.language
                publishedDate = repo.created_at
            }

        fetchJson<Repo list> user url
        |> Job.map (List.map parseRepo)
