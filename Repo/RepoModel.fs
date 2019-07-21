namespace Repo

module Model =
    type Repo = {
        name: string
        html_url: string
        description: string option
        language: string option
        created_at: string
    }
    type RepoResponse = {
        name: string
        url: string
        description: string option
        language: string option
        publishedDate: string
    }
    