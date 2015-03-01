module Executor 
// #r "FSharp.Compiler.Service.dll"

open System
open System.IO
open System.Text
open Microsoft.FSharp.Compiler.Interactive.Shell

type Result = {Output:string; Success:bool}

let TryExecute (code:string) = 
        let defaultArgs = [|"fsi.exe";"--noninteractive";"--nologo";"--gui-"|]
        use inStream = new StringReader("")
        use outStream = new StringWriter()
        use errStream = new StringWriter()

        //let codeWithReference =  [ @"#r ""FSharp.Compiler.Service.dll"""; code] |> String.concat Environment.NewLine

        let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration()
        use session = FsiEvaluationSession.Create(fsiConfig, defaultArgs, inStream, outStream, errStream, collectible=true)
        
        try
            session.EvalInteraction(code)
            {Success = true; Output = outStream.ToString()}
        with e ->
            {Success = false; Output = e.Message}
