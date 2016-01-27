These instructions are for users who would like to get this documentation site running locally,
so you can edit changes and view them immediately.

## Windows

You need these things:

 - this repo - `git clone https://github.com/octokit/octokit.net.git`
 - Python - install it from Chocolatey: `cinst python`
 - mkdocs - install it with pip: `pip install mkdocs`

**NOTE**: of course mkdocs doesn't appear on your PATH after this. Ugh. It's 2016. Come on people.

**HACK**: add this to your PATH - `C:\ProgramData\chocolatey\lib\python3\tools\Scripts` and restart your shell.

Once you've got all that, run `mkdocs serve` from the root of your repository and then point your browser
to `http://localhost:8000/`

**NOTE**:  You'll probably get a dialog here to give `python.exe` permission to open port 8000 on your firewall. This is fine.

## OS X

You need these things:

 - this repo - `git clone https://github.com/octokit/octokit.net.git`
 - Python - if you don't have it already, get it from homebrew: `brew install python3`
 - mkdocs - install it with pip: `pip install mkdocs`

Once you've got all that, run `mkdocs serve` from the root of your repository and then point your browser
to `http://localhost:8000/`
