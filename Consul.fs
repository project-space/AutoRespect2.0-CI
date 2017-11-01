namespace AutoRespect.CI

    module Consul =
        open AutoRespect.CI.Common.Config.Consul
        open AutoRespect.CI.Common.CommandLine
        open AutoRespect.CI.Common.Tools

        let private ``run-under-agent`` action =
            let consul = run (Consul + " agent -dev")
            action()
              
            match consul.HasExited with
                | false -> consul.Kill()
                | true  -> ()
                
        let private ``import-settings`` () =
            let proc = run ("Get-Content " + KV + " | " + Consul + " kv import -")

            proc.WaitForExit()

        let public ``install-consul`` () =
            ``run-under-agent`` ``import-settings``
            ()