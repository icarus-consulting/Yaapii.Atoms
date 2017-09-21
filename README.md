# DOP
Redesign der DOP Software

[![Build status](https://ci.appveyor.com/api/projects/status/5vcpwy657jhc657o/branch/master?svg=true)](https://ci.appveyor.com/project/icarus-consulting/dop/branch/master)
![Build status](https://ci.icarus-consult.de/tecnomatix-ci/api/status/icarus-consulting/dop?branch=master)
# Setup Development Environment
Clone the repository to your local system
```
git clone https://github.com/icarus-consulting/dop.git
```
Make sure you have the latest [NodeJS](https://nodejs.org/en/) and [Yarn](https://yarnpkg.com) installed.
Install all development dependencies from _package.json_.
```
cd dop
yarn install
```

# Running Unit Tests for Tecnomatix
1. Make sure you have Process Simulate installed.
2. Make sure your Z: drive points to the eMPower directory of your Process Simulate installation.
3. Make sure the project yaapii.station.model.tmx.tests contains a config.yml file with content
```
git:
    repo_url: https://github.com/icarus-consulting/tecnomatix-entity-test.git
    local_url: D:\TMX_TESTDATA
    username: <your github username>
    password: <your github password>
```
> The config.yaml file will not be pushed to the repository so your credentials only life on your machine
4. Run the unit tests
