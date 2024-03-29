namespace User

module Model =
    type User = {
        login : string
        avatar_url : string option
        html_url : string
        name : string option
        bio : string option
    }

    type UserOrganization = {
        url : string
    }

    type Organization = {
        avatar_url : string
        html_url : string
    }

    type OrganizationResponse = {
        avatar : string
        url : string
    }

    type UserResponse = {
        username : string
        avatar : string option
        url : string
        name : string option
        bio : string option
        orgs : OrganizationResponse list
    }

    type SearchUser = {
        login : string
        avatar_url : string
    }

    type SearchResult = {
        items : SearchUser list
    }

    type SearchResponse = {
        username : string
        avatar : string
    }
