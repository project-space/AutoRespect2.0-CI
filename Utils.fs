namespace AutoRespect.CI.Utils

    [<AutoOpenAttribute>]
    module FileSystem =
        open System
        open System.IO
        
        let ``temp-directory`` = AppDomain.CurrentDomain.BaseDirectory + "temp\\"

        let ``remove-temp-directory`` () =
            match Directory.Exists (``temp-directory``) with
                | true  -> Directory.Delete(``temp-directory``, true)
                | false -> ()

        let ``remove-file`` (path: string) =
            match File.Exists (path) with
                | true  -> File.Delete (path)
                | false -> ()

        let ``ensure-directory`` (path: string) =
            match Directory.Exists(path) with
                | false -> Directory.CreateDirectory(path) |> ignore
                | true  -> ()
