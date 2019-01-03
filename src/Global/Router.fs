module Router

open Fable.Import
open Fable.Helpers.React.Props
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser

type AtlasPages =
  | Index
  | Map

type Games =
  | Atlas of AtlasPages

type GamePage =
    | Index
    | Game of Games

type Page =
    | Home
    | Games of GamePage

let private toHash page =
    match page with
    | Home -> "#/"
    | Games gamePage ->
        match gamePage with
        | Index -> "#games/index"
        | Game game ->
            match game with
            | Atlas atlasPage ->
                match atlasPage with
                | AtlasPages.Index -> "#games/atlas/index"
                | AtlasPages.Map -> "#games/atlas/map"

let pageParser: Parser<Page->Page,Page> =
  oneOf
    [ map (GamePage.Index |> Games) (s "games" </> s "index")
      map (AtlasPages.Index |> Atlas |> Game |> Games) (s "games" </> s "atlas" </> s "index" )
      map (AtlasPages.Map |> Atlas |> Game |> Games) (s "games" </> s "atlas" </> s "map" )
      map Home top ]

let href route =
  Href (toHash route)

let modifyUrl route =
  route |> toHash |> Navigation.modifyUrl

let newUrl route =
  route |> toHash |> Navigation.newUrl

let modifyLocation route =
  Browser.window.location.href <- toHash route
