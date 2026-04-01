# This is the instructions for the BFG.jar utility
# Make sure JDK...exe and Git...exe are installed
# If the Utilities Folder is currently missing, please make sure to show hidden items in Windows File Storage window

# 1 - Open the GitHub Desktop Application

# 2 - In GitHub Desktop, change your current repoository to your your personal Student File Storage repository

# 3 - In GitHub Desktop's Taskbar (the top part of the window), select Repository > Show in Explorer

# 3 - Open the Utilities folder

# 4 - Left click and drag the bfg.jar file to the folder name on the left of Utilities in the Windows File Explorer address bar (Move to... should pop up) then let go of the left mouse button.

# 5 - Go back to your root GitHub File Storage Folder (You can select it on the address bar or  hit the up arrow next to the address bar).

# 6 - Right Click in the empty space in your GitHub Storage Folder and select Properties

# 7 - Select the Security Tab in the Properties Window and highlight then copy (Right-Click > Copy) the entirity of the Object Name. This is your GitHub file location which will be needed to run the command. Example: ‪C:Users\Documents\GitHub\StudentFileStorage

# 8 - Open the GitHub Desktop Application 

# 9 - On GitHub Desktop's Taskbar, select Repository > Open in Command Prompt

# 10 - Type the following command and hit enter:
git gc

# 11 - Replace "some-big-repo.git" (make sure not to have quotations in your version) in the following command with your GitHub file location (you do not need the .git at the end) and then hit enter: 
	java -jar bfg.jar --strip-blobs-bigger-than 100M some-big-repo.git

# 12 - Once the bfg operation is complete, move the bfg.jar file back to your utilities folder
