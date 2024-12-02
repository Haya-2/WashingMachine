# WashingMachine Manager Application

## Project Overview

This is a WPF (Windows Presentation Foundation) application for managing a queue system for washing machines in a dormitory / residents setting.  
Our application allows a manager to view the status of residents waiting to use washing machines and when they finished, display and change machine availability, and take specific actions like kicking out a resident waiting in a list, but that isn't comming as well as giving key to the residents.  
The app's also have a resident side where they can see usefull informations start / stop a chrno for their machine and most importantly join (and leave) a queue for the machine. 

### Current Limitations 

- **Login Feature**: The login functionality should be fully operational. There is still some error cases that hasn't been tested. There are buttons to navigate for now, so everything is accessible.
- **Button Functionality**: Some buttons, such as "Give Key" and "Kick Out," have not yet been implemented with corresponding commands or actions. There is also the "Declare state Machines" as well as "Daily repport". The latter will be a json of all the mouvement for the machines, so the manager can use this to ensure residents will pay for the machine.

## Setup Instructions

1. **Clone the Repository**:
    ```bash
    git clone git@github.com:Haya-2/WashingMachine.git
    ```
2. **Open in Visual Studio separetly API and APP**:
    - Open both solutions files in Visual Studio.
3. **Run the API**:
    - Build the solution API and running it.
4. **Run the Application**:
    - Build the solution and run the application in Visual Studio.
Note : The order can be inverted BUT it can lead to slowing down computer on bad configuration. 

## Project Structure

- **ViewModels**: Contains the logic for binding data to the UI, including managing resident data or machine availability.
- **Views**: XAML files that define the user interface, including the layout and styling of elements in the views.
- **Models**: Classes representing data structures for residents, machines, etc.

## Future Enhancements

- **Button Functionality**: Implement the actions for "Give Key" and "Kick Out" buttons, allowing managers to effectively control the washing machine access queue.
- **Error Handling and Notifications**: Add user feedback for successful actions or errors to improve usability.
- **Add user features** : Such as their profile, their stat, but also guides related to resident or building and machine usage.

# 02/12/2024 - Fix to do : 

- Manager side, the connection to the data has been compromised. I (Aline) will work on it soon.


Author : Aline (Haya-2)  
Co-author : Cassandra (ks0214)
