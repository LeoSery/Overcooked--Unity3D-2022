# Overcooked--Unity3D-2022 : 
Game made during my bachelor 3 year (2022/2023) for a Unity3D module at Ynov Bordeaux.

## Summary :

- Presentation
- Game keys
- Main mechanics
- How to open the project
- How to build the project

## Presentation :

You play as a character who can use **the ingredients at his disposal** to **make the recipes** that arrive.

You need to **get the vegetables** from a vending machine and then cut them. Then if the **vegetable can be cooked**, you need to **put it in a pan** and then **put the pan on the stovetop** to cook the ingredient.

Once your ingredient is cooked, you can **put it on a plate**, so you can **combine it with other foods**. Once your plate **contains all the ingredients called for in the recipe**, you can **take the dish to the delivery counter**.

## Game keys :

- *Movement*:
    -  Move forward: **Z**
    - Move to the left: **Q**
    - Move to the right: **D**
    - Move downward: **S**

- *Objects*:
    -  Pick/Drop: **Space**

- *Interact with devices*:
    - Add an object: **E**
    - Use the device on the object: **R**
    - Retrieve object after use: **F**

## Main mechanics : 

### Food recovery :

There are **3 types** of food :

    - Potato
    - Steak
    - Salad

You can retrieve the different foods from their respective dispensers.

When you are near a dispenser, press the **`E`** key to get a food item.

### Food cutting :

These 3 elements can be cut with the cutting board.

 - **Place the food on the board**:

When you have a food in your hands, you are close to a cutting board, you can press the `E` button to put the food on the board.

- **Using the cutting board**: 

Once your food is on the board. You can hold the `R` button for 3 seconds to cut the food into its cut shape.

Different shapes of cut food :

    - Potato > Fries
    - Steak > Sliced Steak
    - Salad > Sliced Salad
    
- **Recovery of the ingredient**:

You can retrieve your food from the cutting board at any time, whether it is already cut or not.

You can't use the cutting board if an item like a plate or a pan is placed on the cutting board.

### Put the food in the pan :

- **Put the food in the pan** :

Once you have your cut food in your hands you can place it in a pan before cooking it.

To do this stand next to a pan with food in your hands and press **`E`** to place the food in the pan.

You can remove the food from the pan by pressing the **`F`** key near the pan.

Next you can grab the pan to move it by pressing **`Space`** to put it in the character's hands.

Please note that uncut food and cut salad cannot be placed in the pan.

- **Cooking of food** :

Once your food is in the pan, it needs to be cooked. To do this, approach a gas stove with a full pan and press the **`E`** button to place it on the stovetop.

Once your pan is placed, hold the **`R`** button for 3 seconds to cook your food. 

Once cooked, your **food will change texture**:

    - Fries > Cooked fries
    - Sliced steak > Cooked steak
    - Salad > CANNOT BE COOKED
    
When your food is cooked, press the **`Space`** button to retrieve the pan.

### Putting food on plates :

After your food is cooked, you can retrieve it by setting your pan down and pressing the **`F`** button.

Move to your plate and press the **`E`** button again to place your cooked food on your plate. You can put more than one food on a plate to make your dish.

The order of the ingredients on the plate does not matter, but if you want to remove a food item, press the **`F`** key, which will remove the last food item placed on the plate.

### Delivering the finished dish :

When your plate composition is finished, you must bring it to the delivery area.

Hold your plate in your hands with the **`Space`** key. Move to the delivery area and press the **`E`** key to place your dish in the area.

If your dish matches the recipe on the screen, you get money, otherwise you lose.

## How to open the project:

- Clone the git repository to your computer with the following command :
```
git@github.com:LeoSery/Overcooked--Unity3D-2022.git
```
or 
```
https://github.com/LeoSery/Overcooked--Unity3D-2022.git
```

- open Unity Hub and do "*Add project from disk*"

    Select "`..\Overcooked--Unity3D-2022`"

- Check that the project opens with the Unity editor in version "**2022.1.21f1**"

## How to build the project : 

- Once the project is open in Unity, do *"File" > "Build Settings"*

- In the window that has just appeared, in the *"Scenes In Build"* section, make sure that *"scenes/Game"* is checked.

- for the platform choose: *"PC, Mac & Linux Standalone"*

- then choose your platform in *"Target Platform"*

- and finally press *"Build And Run"*
