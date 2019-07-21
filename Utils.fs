module Utils

open System
open System
open FSharp.Json
open Hopac
open HttpFs.Client
open Suave.Http
open Suave.Writers
open Suave.Operators

let config = JsonConfig.create(serializeNone = SerializeNone.Omit)

let fromJson<'a> = Json.deserializeEx<'a> config

let toJson<'a> = Json.serializeEx config

let getBody<'a> (req : HttpRequest) =
    let getString (rawForm: byte[]) =
        System.Text.Encoding.UTF8.GetString(rawForm)
    req.rawForm |> getString |> fromJson<'a>

let setCORSHeaders =
    setHeader  "Access-Control-Allow-Origin" "*"
    >=> setHeader "Access-Control-Allow-Headers" "content-type"
    
let trace a =
    printf "%s" a
    a
    
let fetchJson<'a> user url  =
    let token = Environment.GetEnvironmentVariable("TOKEN")
    Request.createUrl Get url
    |> Request.setHeader(UserAgent user)
    |> Request.setHeader(Custom ("Authorization", String.Format("token {0}",token)))
    |> Request.responseAsString
//    |> Job.map trace
    |> Job.map fromJson<'a>