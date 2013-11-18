using System;

namespace Octokit
{
    //File JSON Example
    /*
    {
        "type": "file",
        "encoding": "base64",
        "size": 5362,
        "name": "README.md",
        "path": "README.md",
        "content": "encoded content ...",
        "sha": "3d21ec53a331a6f037a91c368710b99387d012c1",
        "url": "https://api.github.com/repos/pengwynn/octokit/contents/README.md",
        "git_url": "https://api.github.com/repos/pengwynn/octokit/git/blobs/3d21ec53a331a6f037a91c368710b99387d012c1",
        "html_url": "https://github.com/pengwynn/octokit/blob/master/README.md",
        "_links": {
            "git": "https://api.github.com/repos/pengwynn/octokit/git/blobs/3d21ec53a331a6f037a91c368710b99387d012c1",
            "self": "https://api.github.com/repos/pengwynn/octokit/contents/README.md",
            "html": "https://github.com/pengwynn/octokit/blob/master/README.md"
        }
    }
    */

    //Directory JSON example
    /*
    [
        {
            "type": "file",
            "size": 625,
            "name": "octokit.rb",
            "path": "lib/octokit.rb",
            "sha": "fff6fe3a23bf1c8ea0692b4a883af99bee26fd3b",
            "url": "https://api.github.com/repos/pengwynn/octokit/contents/lib/octokit.rb",
            "git_url": "https://api.github.com/repos/pengwynn/octokit/git/blobs/fff6fe3a23bf1c8ea0692b4a883af99bee26fd3b",
            "html_url": "https://github.com/pengwynn/octokit/blob/master/lib/octokit.rb",
            "_links": {
                "self": "https://api.github.com/repos/pengwynn/octokit/contents/lib/octokit.rb",
                "git": "https://api.github.com/repos/pengwynn/octokit/git/blobs/fff6fe3a23bf1c8ea0692b4a883af99bee26fd3b",
                "html": "https://github.com/pengwynn/octokit/blob/master/lib/octokit.rb"
            }
        },
        {
            "type": "dir",
            "size": 0,
            "name": "octokit",
            "path": "lib/octokit",
            "sha": "a84d88e7554fc1fa21bcbc4efae3c782a70d2b9d",
            "url": "https://api.github.com/repos/pengwynn/octokit/contents/lib/octokit",
            "git_url": "https://api.github.com/repos/pengwynn/octokit/git/trees/a84d88e7554fc1fa21bcbc4efae3c782a70d2b9d",
            "html_url": "https://github.com/pengwynn/octokit/tree/master/lib/octokit",
            "_links": {
                "self": "https://api.github.com/repos/pengwynn/octokit/contents/lib/octokit",
                "git": "https://api.github.com/repos/pengwynn/octokit/git/trees/a84d88e7554fc1fa21bcbc4efae3c782a70d2b9d",
                "html": "https://github.com/pengwynn/octokit/tree/master/lib/octokit"
            }
        }
    ]
     */

    public class ContentsResponse
    {
        public string ResponseType { get; set; }

        public int Size { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Content { get; set; }

        public string Sha { get; set; }


    }
}
