open System
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open dotenv.net

DotEnv.Config()
    
let app =
    choose
        [
            pathStarts "/api" >=> choose [
                GET >=> path  "/api/hello" >=> OK "hello"
            ]
            Repo.Controller.routes
            User.Controller.routes
        ]
            

[<EntryPoint>]
let main argv =
    let port = Environment.GetEnvironmentVariable("PORT") |> uint16
    startWebServer 
         { defaultConfig with
             bindings = [ HttpBinding.create HTTP System.Net.IPAddress.Any port ] } 
             app
    0