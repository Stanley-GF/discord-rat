# Discord Rat

Just a remote administration tool controllable with a Discord Bot using Discord.Net

# Setup

* Open the ".csproject file"

* Install via nuget package "Discord.Net" => Version 1.0.1  

* Go in Program.cs and edit "discord bot token here" by your discord bot token 

![](https://cdn.discordapp.com/attachments/749668330775904387/751911305556000788/unknown.png)

# Features

`!!check` : list all connected client

`!!screen <ip>` : take a screenshot of the screen of the selected computer

`!!deletefile <ip> <path>` : delete the specific file from the specific computer

`!!shutdown <ip>` : Shutdown the selected computer

`!!getpath <ip>` : get the global path of the ip selected

`!!start <ip> <name> | <cmd command>` : start applicaton / execute cmd command

`!!message <ip> <message>` : send a messagebox message on the victim computer

`!!uploadfile <ip> <path>` : upload a specific file from the victim computer in the context channel

