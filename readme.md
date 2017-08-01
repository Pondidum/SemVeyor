
# SemVer Surface Surveyor

* cli
  * usage `survey.exe --config ./semveyor.json assembly.dll`
* storage
  * format (default/local)
    * sqlite file?
    * json?
* storage drivers perhaps?
  * allow for non-local storage
  * useful for coordinating build agents etc
* scanned data
  * all public types
  * all public methods, constants, properties, variables, constructors on a type
* reports
  * what things have changed
    ```
    namespace.of.a.class
      + SomeMethod() => Bump Minor
      + SomeConst => Bump Minor
    some.other.namespace
      - SomeProperty => Bump Major
    ```
  * what caused a semver bump
* reporting drivers?
  * argument passing same as storage drivers
* status codes
  * arguments for specifying too perhaps?

## Configuration

```json
{
    "readonly": true,
    "storage": {
      "file": {
        "path": "history.lsj"
      },
      "aws-s3": {
        "key": "some/file.lsj",
        "accesskey": "",
        "secretkey": ""
      }
    },
    "reporters": {

    }
}
```



## Todo

* [ ] combine cli and options file
* [ ] tests for cli options
* [ ] support readonly flag on cli




# Config paths

* `semveyor.exe` & `semveyor.json` doesnt exist
  * no `asm path` => throw
  * with `asm path` => default config
* `semveyor.exe` & `semveyor.json` exists => use `semveyor.json`
* `semveyor.exe --config blah.json` doesnt exists => `throw new FileNotFoundException`
* `semveyor.exe --config blah.json` exists => use `blah.json`
