FROM microsoft/dotnet:2-sdk AS build-env
WORKDIR /app

# copy fsproj and restore as distinct layers
COPY *.fsproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -r linux-x64 -o out

# build runtime image
FROM microsoft/dotnet:2-runtime-deps
WORKDIR /app
COPY --from=build-env /app/out ./
COPY .env .
ENTRYPOINT ["./GithubApp"]