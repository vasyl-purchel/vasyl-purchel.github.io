module App.View

open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.FontAwesome
open State
open Types
open Fulma

open Tiles.Types

let private sourceCodeButton =
  Button.a [ Button.Props [ Href Constants.GithubPage ] ]
           [ Icon.icon [ ] [ Fa.i [ Fa.Brand.Github ] [ ] ]
             span [ ] [ str "Source" ] ]

let private navbarEnd =
    Navbar.End.div [ ]
        [ Navbar.Item.div [ ]
            [ Field.div [ Field.IsGrouped ]
                [ Control.p [ ] [ sourceCodeButton ] ] ] ]

let private navbarStart dispatch =
  let games =
    let atlasOnClick _ =
      Router.AtlasPages.Index |> Router.Atlas |> Router.Game |> Router.Games
      |> Router.modifyLocation
    Navbar.Item.div [ Navbar.Item.HasDropdown; Navbar.Item.IsHoverable ] [
      Navbar.Link.div [ ] [ str "Games" ]
      Navbar.Dropdown.div [ ] [
        Navbar.Item.a [ Navbar.Item.Props [ OnClick atlasOnClick] ] [ str "Atlas" ]
      ] ]

  Navbar.Start.div [ ] [ games ]

let private navbarView isBurgerOpen dispatch =
  let sourceCodeNavItem =
    Navbar.Item.a [ Navbar.Item.Props [ Href Constants.GithubPage ]
                    Navbar.Item.CustomClass "is-hidden-desktop" ]
                  [ Icon.icon [ ] [ Fa.i [ Fa.Brand.Github; Fa.Size Fa.FaLarge ] [ ] ] ]

  div [ ClassName "navbar-bg" ]
      [ Container.container [ ]
          [ Navbar.navbar [ Navbar.CustomClass "is-primary" ]
              [ Navbar.Brand.div [ ]
                  [ Navbar.Item.a [ Navbar.Item.Props [ Href "#" ] ]
                      [ Heading.p [ Heading.Is4 ]
                          [ str "Corpusculum" ] ]
                    // Icon display only on mobile
                    sourceCodeNavItem
                    // Make sure to have the navbar burger as the last child of the brand
                    Navbar.burger [ Fulma.Common.CustomClass (if isBurgerOpen then "is-active" else "")
                                    Fulma.Common.Props [
                                      OnClick (fun _ -> dispatch ToggleBurger) ] ]
                      [ span [ ] [ ]
                        span [ ] [ ]
                        span [ ] [ ] ] ]
                Navbar.menu [ Navbar.Menu.IsActive isBurgerOpen ]
                  [ navbarStart dispatch
                    navbarEnd ] ] ] ]

let private atlasMapOverview =
  div [ ]
      [ str "Currently there are not much information on Atlas (playatlas.com), so I've decided to create an interactive map that would get automatically available information from steam plus support custom markers"
        br [ ]
        Button.a [ Button.Props [ Router.href (Router.AtlasPages.Map |> Router.Atlas |> Router.Game |> Router.Games) ]
                   Button.Color IsInfo ]
                 [ str "Check it out!" ] ]

let private renderPage model dispatch =
    match model with
    | { CurrentPage = Router.Page.Home } ->
        Tiles.View.root { Tiles = [
          { Title = "Hello"
            Content = str "Hi, I'm playing with some UI development and this is is a result of such \"coding for fun\"" }
          { Title = "Atlas Map with sprinkles"
            Content = atlasMapOverview } ] }
    | { CurrentPage = Router.Page.Games page } -> str <| sprintf "Game %A" page
    | _ -> Render.pageNotFound

let private root model dispatch =
    div [ ]
        [ navbarView model.IsBurgerOpen dispatch
          renderPage model dispatch ]


open Elmish.React
open Elmish.Debug
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Elmish.HMR

Program.mkProgram init update root
|> Program.toNavigable (parseHash Router.pageParser) urlUpdate
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReact "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
