namespace Repo

module Service =
    open Utils
    open Hopac
    open System
    open Model
    open Config
        
    let fetchRepo (user:string) =
        let url = String.Format("{1}/{0}/repos?sort=created&direction=desc&type=sources", user, BASE_URL)
        let parseRepo (repo: Repo) =
            {
                RepoResponse.name = repo.name
                url = repo.html_url
                description =  repo.description
                language = repo.language
                publishedDate = repo.created_at
            }
            
        fetchJson<Repo list> user url
        |> Job.map(List.map parseRepo)
