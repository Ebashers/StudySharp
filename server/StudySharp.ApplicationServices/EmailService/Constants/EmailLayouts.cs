namespace StudySharp.Domain.Constants
{
    public class EmailLayouts
    {
        public const string Default = @"<!DOCTYPE html>
                    <html lang='en'>

                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <meta http-equiv='X-UA-Compatible' content='ie=edge'>
                        <title>{0}</title>
                        <style type=""text/css"">
                            a[x-apple-data-detectors] {{color: inherit !important;}}
                        </style>
                    </head>

                    <body>

                            <div style='padding-top: 15px;'>
                                <h1>{1}</h1>
                            </div>

                    </body>";
    }
}
