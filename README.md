# TvMazeScraper
A scraper that scrapes the some data from the TvMaze public api. Exposes an api to the scraped data as well.

## Prerequisites
1. Install postgres ([https://www.postgresql.org/](https://www.postgresql.org/))
2. Install .NET Core 2.1 ([https://dotnet.microsoft.com/download/dotnet-core/2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1))

Some explanations for the different projects:

## TvMazeScraper.ApiClient
This project exposes a REST api (not completely rest as it's missing some items which are needed to make the api completely REST) for the scraped data. The swagger interface can be found at "/swagger". Supports a page number and the page size. To run this project do the following:

1. CMD to the working directory of this project (ex: D:\Projects\TvMazeScraper\TvMazeScraper.ApiClient)
2. Execute command: dotnet run TvMazeScraper.ApiClient.csproj

## TvMazeScraper.Scraper
This project scrapes the public api of TvMaze ([http://www.tvmaze.com/api](http://www.tvmaze.com/api)). Stores the scraped data in a postgres database. This is also the project which has the migrations asociated with it. To do a migration do the following:


1. CMD to the working directory of this project (ex: D:\Projects\TvMazeScraper\TvMazeScraper.Scraper)
2. Execute command: dotnet ef database update
3. Execute command: dotnet run TvMazeScraper.Scraper.csproj

On startup the scraping will immediately begin. This can take a while, so sit back and relax. After the initial run, every hour a new scrape will begin. If you want to force a scrape yourself, navigate to "/swagger". An endpoint is available to force a scrape.
