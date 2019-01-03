module Tiles.Types

open Fable.Import.React

type Tile =
  { Title: string
    Content: ReactElement }

type Model =
  { Tiles: Tile list }
