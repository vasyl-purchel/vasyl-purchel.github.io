module App.State

open Elmish
open Types
open Fable.Import

let urlUpdate (result: Option<Router.Page>) model =
  match result with
  | None ->
      Browser.console.error("Error parsing url: " + Browser.window.location.href)
      model, Router.modifyUrl model.CurrentPage
  | Some page -> { model with CurrentPage = page }, Cmd.none

let init result =
  urlUpdate result Model.Empty

let update msg model =
    match (msg, model) with
    | (ToggleBurger, _) ->
        { model with IsBurgerOpen = not model.IsBurgerOpen }, Cmd.none
