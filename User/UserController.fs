namespace User

module Controller =
    open Hopac
    open Suave
    open Suave.Successful
    open Suave.Filters
    open Suave.Operators
    open Service
    open Utils

    let user = fetchUser  >> run >> toJson >> OK
    let search = searchUser >> run >> toJson >> OK
    let routes = choose [
        GET >=> setCORSHeaders >=> pathScan "/user/%s" user
        GET >=> setCORSHeaders >=> pathScan "/search/%s" search
    ]