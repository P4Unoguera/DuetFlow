# Duet Flow - Full-Body Interactive Experience

## Introduction
Duet Flow is a full-body interactive virtual reality experience developed as part of the Interactive Systems course in the Audiovisual Systems Engineering Degree. The project was created by Xavier Riera, Jordi Rubio, and Pau Noguera. The course has a strong practical orientation and focuses on the design and implementation of real-time full-body interactive experiences using the Mixed Reality system of the Full-Body Interaction Laboratory (FuBIntLab), part of the Cognitive Media Technologies research group in the UPF. The experience was developed using Unity3D, a professional tool widely used in Virtual, Augmented, and Mixed Reality applications and using the VIVE Tracker(3.0) controllers.

Duet Flow is a virtual reality game for two players, where each participant competes by playing a 12-key piano with their foot. The winner is the one who best follows the rhythm and hits the most correct notes.

## Game Mechanics:
**Title Scene**

The game begins in the "Title" scene, with an initial animation and a large test keyboard where two players can play freely. From this initial scene, players can choose whether to press play or stop playing.
Scripts from the *TitleScene* and *Buttons* folders are used here.

**Selection Scene**

There are 3 melody options, depending on the level the players want. Once a melody is selected, the game redirects to the Competition Mode scene.
Scripts from the *Buttons* and *Selector* folders are used in this part.

**Competition Mode Scene**

Players position themselves on their respective platforms. Then, a countdown appears, and the game begins. Scripts from the rest of folders are used for these functionalities.

## Acknowledgements:

This project is based on the *Interactive Systems* Unity template developed by **Pau Amatller** and **Pedro Omedas** from the **Full-Body Interaction Laboratory (FuBIntLab)**. The original template is released under the **MIT License** and is available on GitHub via the PauAmatller repository. We acknowledge and thank the authors for providing the base framework used in this project.

- https://github.com/PauAmetller/InteractiveSystemsUpdatedTemplate


In this project, we mostly used our own sounds, along with FL Studio libraries and the LABS Soft Piano. We have also used some sounds that we would like to acknowledge:

- Click Tick.wav by malle99 -- https://freesound.org/s/384187/ -- License: Creative Commons 0

- Button Click / Selection.wav by aphom000 -- https://freesound.org/s/623175/ -- License: Creative Commons 0

- Applause 50 seconds into interval by iainmccurdy -- https://freesound.org/s/681095/ -- License: Attribution 4.0

We also have used some external assets downloaded from the internet:

- Piano 3D Model by shakiller -- https://sketchfab.com/3d-models/piano-aafcb75617a44ccf9e6aedc3cb0b638b

- Star 3D Model by BenjaTheMaker -- https://assetstore.unity.com/packages/3d/props/simple-gems-and-items-ultimate-animated-customizable-pack-73764

- Dynamic Space Background by DinV Studio -- https://assetstore.unity.com/packages/2d/textures-materials/dynamic-space-background-lite-104606
