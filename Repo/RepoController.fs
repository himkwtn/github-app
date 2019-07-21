namespace Repo

module Controller =
    open Hopac
    open Suave
    open Suave.Successful
    open Suave.Filters
    open Suave.Operators
    open Service
    open Utils

    let listRepo = fetchRepo >> run >> toJson >> OK 
    let routes = choose [
        GET  >=> setCORSHeaders >=> pathScan "/repo/%s" listRepo

    ]