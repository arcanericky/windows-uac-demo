##Windows UAC Demo
---
This project provides a programmatic example of how to execute code with elevated privileges This involves launching a process with the "runas" verb set. In short:
```
ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd");
processStartInfo.Verb = "runas";
Process process = Process.Start(processStartInfo);
```
See the code in the project for more information and a complete working example.