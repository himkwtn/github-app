## Description
This is an API for fetching public user data from github.

Written in F# with [Suave](https://suave.io/) framework.

Currently, there are 3 endpoints:
- /search/{username}
  - search for given username and return list of users that best match
- /user/{username}
  - fetch data of that user
- /repo/{username}
  - fetch list of repos owned by that user

