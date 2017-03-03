
# SemVer Surface Surveyor

* cli
  * usage `survey.exe [options] assembly.dll`
  * options
    * `storage` - optional, controls which storage driver to use
    * `storage:*` - `*` can be anything, passes to the storage driver?
* storage
  * format (default/local)
    * sqlite file?
    * json?
  * `storage:path` path to the file
* storage drivers perhaps?
  * allow for non-local storage
    * useful for coordinating build agents etc
  * argument passing:
    * `-storage 'aws-s3'` - would find an assembly `./stores/remote.dll`
    * `-storage:accesskey '2312313'`
    * `-storage:secretkey '123123'`
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
  * `-failureCode 1`
  * `-successCode 0`



## Rules

* Anything added => minor++
* Anything removed => major++
* Changed == [added + removed] => major++
* Do method overloads need to be considered


* Type added => minor++
* Type removed => major++
* Type became more visible => minor++
* Type became less visible => major++


* Type had a visible ctor added => minor++
* Type had a visible ctor removed => major++
* Type had a ctor more visible => minor++
* Type had a ctor less visible => major++



## Test Cases

### Adding a generic argument to one overloaded method
```
public class PublicEmptyClass
{
	public void Execute() {}
	public void Execute(string name) {}
}
```
to
```
public class PublicEmptyClass
{
	public void Execute() {}
	public void Execute<T>(string name) {}
}
```
