# Plugin_demo
A project to demonstrate how Plug-ins work

## Structure:

1. A Shared library (MainApplication.PluginBase). Contains contracts and shared models
2. An Application (MainApplication.App), in this case, also the PluginHost (loads and calls the plugins)
3. Plugins: ClassLibrary projects to be loaded by the host

How to use:

* Compile everything
* Put all plugins Dlls inside the App/Host plugins folder (the one inside /bin/...)
* Run the host
