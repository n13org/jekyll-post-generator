# Jekyll-post-generator
Create random posts for jekyll with lorem ipsum text

## Nuget
https://www.nuget.org/packages/NLipsum

## Usage:
`dotnet run 2017-01-01 2020-03-31 75 ".\posts\generated"`

`jekyll-post-generator 2017-01-01 2020-03-31 75 ".\posts\generated"`

## Publish
WIN: dotnet publish -c Release -r win10-x64 --self-contained
WIN .NET 6: dotnet publish -c Release -r win10-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
LINUX: dotnet publish -c Release -r ubuntu.16.10-x64

## Links:
* https://dotnetcoretutorials.com/2019/06/20/publishing-a-single-exe-file-in-net-core-3-0/
* https://dotnetcoretutorials.com/2019/06/27/the-publishtrimmed-flag-with-il-linker/

## ToDos:
1. https://github.com/jpdillingham/Utility.CommandLine.Arguments
2. Category Shuffle: Random 1 to x
3. Tags Shuffle: Random 1 to x
4. Add same images
