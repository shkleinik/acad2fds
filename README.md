# acad2fds

AutoCAD to [FDS](https://github.com/firemodels/fds) ( Fire Dynamics Simulator ) bridge. Plugin provides possibility to convert AutoCAD geometry to FDS format, automate calculation in FDS and view calculation results in AutoCAD. 
You are welcome to view [AutoCAD to FDS Presentation](https://github.com/shkleinik/acad2fds/raw/master/assets/Home_Acad2Fds%20presentation.ppt) (approx 4Mb).

Some simple [geometry samples](https://github.com/shkleinik/acad2fds/raw/master/assets/Home_Plugin%20Samples.zip).

# Overview

## Plugin's Menu

![Menu](https://github.com/shkleinik/acad2fds/blob/master/assets/Home_Menu.png)

## Plugin Options

![Options](https://github.com/shkleinik/acad2fds/blob/master/assets/Home_settings.png)

## Converting Geometry

### Step 1: Open required drawing

### Step 2: Click "Start calculation" button in plugin menu. In Calculation Info window set Output Path and Calculation Time parameters.

![Calculation](https://github.com/shkleinik/acad2fds/blob/master/assets/Home_calculation_info.png)

**Tip:** if you just want to check if geometry is converted correctly you can set Calculation Time parameter to "0".

### Step 3: Select objects you want to convert and press Enter.

![Select objects](https://github.com/shkleinik/acad2fds/blob/master/assets/Home_select_objects.png)

Wait while our engine convert all geometry.

![Wait](https://github.com/shkleinik/acad2fds/blob/master/assets/Home_Progress.png)

### Step 4: Results Viewing

To view results navigate plugin menu and click 'View Results in SmokeView' menu item. Open File dialog will appear. Select required *.smv file. Selected file will be opened in AutoCAD.

![Result](https://github.com/shkleinik/acad2fds/blob/master/assets/Home_smv_integration.png)
