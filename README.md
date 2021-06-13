[![Build 
Status](https://travis-ci.com/KazuOfficial/Betacraft-Launcher.svg?branch=master)](https://travis-ci.com/KazuOfficial/Betacraft-Launcher)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/KazuOfficial/Betacraft-Launcher">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Betacraft Launcher</h3>

  <p align="center">
    A nostalgic launcher that brings back support for older Minecraft versions!
    <br />
    <a href="https://betacraft.pl/"><strong>Website</strong></a>
    <a href="https://github.com/KazuOfficial/Betacraft-Launcher"><strong>Explore the docs (coming soon) »</strong></a>
    <br />
    <br />
    <a href="https://github.com/KazuOfficial/Betacraft-Launcher/releases">Download</a>
    ·
    <a href="https://github.com/KazuOfficial/Betacraft-Launcher/issues">Report Bug</a>
    ·
    <a href="https://github.com/KazuOfficial/Betacraft-Launcher/pulls">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <a href="#history-of-the-project">History Of The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

## About The Project

![alt text](https://github.com/KazuOfficial/BetaCraft-Launcher/blob/master/images/launcher.png)

Enjoy your favourite old Minecraft versions exactly as you remember them!

* Choose a version of your desire from the version list
* Pick any username you wish
* Enjoy the legacy skin, texture, and sound support thanks to built in Betacraft Proxy
* Customize your instance by changing instance size, instance name, Betacraft Proxy and Discord RPC settings
* Show your Discord friends what you're doing thanks to Discord Rich Presence implemenation

Check out the main, supported edition of Betacraft Launcher: [https://github.com/Moresteck/BetaCraft-Launcher-Java](https://github.com/Moresteck/BetaCraft-Launcher-Java)

<!-- ABOUT THE PROJECT -->
##### History Of The Project

Back in 2018 Betacraft, a Minecraft server running on version b1.7.3 was about to be transformated into a modded Minecraft server. Me and Moresteck, the adminsitrators of Betacraft, knew that people won't be willing to install mods on their own, therefore we acknowledged the fact that there will be less people playing on our server. We wanted to prevent that, so the idea of Betacraft Launcher was born.

The first public version of the launcher was able to download and run a premade modded Minecraft version prepared specifally for our server. The idea of a simple launcher made specifally for a small Minecraft server grew into a mission of providing the best nostalgic Minecraft experience out there.

Now Betacraft consists of:
* A Polish Minecraft server running on version b1.7.3 (ip: betacraft.pl).
* Betacraft Launcher, which with the help of Betacraft Proxy introduces various fixes and addons for older Minecraft versions.
* Betacraft Proxy, which brings back support for skins, sounds, and textures on older Minecraft versions.
* Betacraft Website, which allows you to change your skin and cape in Betacraft Proxy, and which is a home for all Betacraft products.

Visit our website: [https://betacraft.pl/](https://betacraft.pl/)

### Built With

* .NET 5
* WPF
* [Caliburn Micro](https://caliburnmicro.com/)
* [Serilog](https://serilog.net/)
* [Discord RPC C#](https://github.com/Lachee/discord-rpc-csharp)

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/KazuOfficial/Betacraft-Launcher.git
   ```
2. Create `appsettings.json` in BetacraftLauncher folder
3. Enter your Discord Client ID in `appsettings.json`. If you don't have the ID you can get one from [https://discord.com/developers/applications](https://discord.com/developers/applications)
   ```
   "discordClientKey": "DISCORD_CLIENT_KEY"
   ```

<!-- USAGE EXAMPLES -->
## Usage

1. Click `Version List` and select a version of your desire.
2. Type in your username in the TextField.
3. Click `Play` and enjoy!
 
You can also change some settings in the `Instance Settings` tab.

_For programmers, please refer to the [Documentation](https://example.com)_


<!-- CONTRIBUTING -->
## Contributing

Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the GNU License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Email - kazuofficial.contact@gmail.com
Discord - Kazu#8828

# Have a wonderful day :)