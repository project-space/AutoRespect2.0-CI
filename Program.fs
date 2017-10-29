open AutoRespect.CI.Consul
open AutoRespect.CI.Utils

[<EntryPoint>]
let main argv =
    ``install-consul``() |> ignore
    ``remove-temp-directory``()

    0 