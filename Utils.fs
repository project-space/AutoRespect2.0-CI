namespace AutoRespect.CI.Utils

    module CommandLine =
        open System.Diagnostics

        let run command =
            let powerShellProcess = new Process() 
            powerShellProcess.StartInfo.FileName <- @"%SystemRoot%\system32\WindowsPowerShell\v1.0\powershell.exe"
            powerShellProcess.StartInfo.RedirectStandardOutput <- true
            powerShellProcess.StartInfo.Arguments <- command

            powerShellProcess.Start() |> ignore
            powerShellProcess.WaitForExit()
            powerShellProcess.StandardOutput.ReadToEnd()

    module Network =
        open System
        open System.Net
        let private http = new WebClient()

        let ``download-file`` uri ``file-name`` =
            http.DownloadFile(Uri(uri), ``file-name``)

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
