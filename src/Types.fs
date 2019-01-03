module App.Types

type Model =
  { CurrentPage : Router.Page
    IsBurgerOpen : bool }

  static member Empty =
    { CurrentPage = Router.Page.Home
      IsBurgerOpen = false }

type Msg =
  | ToggleBurger
