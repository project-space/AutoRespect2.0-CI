namespace AutoRespect.CI.Consul

    [<AutoOpenAttribute>]
    module Consul =
        open System
        open System.Net
        open System.IO.Compression
        open AutoRespect.CI.Utils

        let private ``download-consul`` () =
            let ``consul-1.0.0-windows-x64-url`` = "https://releases.hashicorp.com/consul/1.0.0/consul_1.0.0_windows_amd64.zip?_ga=2.90675027.879255611.1509304178-525967340.1509304178";
            let ``file-name`` = "consul-1.0.0-windows-x64.zip"
            let ``download-path`` = AppDomain.CurrentDomain.BaseDirectory + @"temp\";
            let destination = ``download-path`` + ``file-name``

            ``ensure-directory`` ``download-path``
            ``remove-file`` destination

            let client = new WebClient()
            let response = client.DownloadFile(Uri(``consul-1.0.0-windows-x64-url``), destination);

            destination
            
        let private ``unzip-consul`` (``path-to-zip``) =
            let ``extract-path`` = AppDomain.CurrentDomain.BaseDirectory + "./tools/"
            let ``consul-executable`` = (``extract-path`` + "consul.exe")

            ``ensure-directory`` ``extract-path``
            ``remove-file`` ``consul-executable``

            ZipFile.ExtractToDirectory(``path-to-zip``, ``extract-path``)

            ``consul-executable``

        let public ``install-consul`` () =
            let ``path-to-zip`` = ``download-consul``()
            let ``consul-executable`` = ``unzip-consul`` ``path-to-zip``

            ``consul-executable``
