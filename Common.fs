namespace AutoRespect.CI.Common

    module Config =
        module Consul = 
            let KV = __SOURCE_DIRECTORY__ + "\\config\\consul\\kv.json"

    module Tools = 
        let Consul = __SOURCE_DIRECTORY__ + "\\tools\\windows-x64\\consul.exe" 

    module CommandLine =
        open System.Diagnostics
        open System

        let private onReceiveOutput (event:DataReceivedEventArgs) =
            Console.WriteLine(event.Data)
            ()

        let run command =
            let powerShellProcess = new Process() 
            powerShellProcess.StartInfo.FileName <- @"C:\Windows\system32\WindowsPowerShell\v1.0\powershell.exe"
            powerShellProcess.StartInfo.RedirectStandardOutput <- true
            powerShellProcess.StartInfo.Arguments <- command

            powerShellProcess.OutputDataReceived.Add(onReceiveOutput) 
            powerShellProcess.Start() |> ignore
            powerShellProcess