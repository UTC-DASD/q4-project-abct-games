# This is the command instructions to be used after git.exe has been installed. For ease of access for commands, there is a list at the bottom.

# The autocrlf commands will change the autocrlf  to false so it does not throw any errors while committing files

# The core.longpaths command will allow for longer file names as each file name always includes the location of the object. If it is a file that is nested underneath several folders, it will include the drive letter, all of the previous folders, the file name, and the file extension in the name of the objects

# 1 - Open GitHub Desktop and set the current repository your personal Student File Storage repository

# 2 - On GitHub Desktop's Taskbar, select Repository > Open in Command Prompt

# 3 - Highlight, copy, then paste the following command:
git config --global core.autocrlf false

# 4 - Highlight, copy, then paste the following command:
git config --system core.autocrlf false

# 5 - Highlight, copy, then paste the following command:
git config --local core.autocrlf false

#6 Highlight, copy, then paste the following command:
git config --global core.longpaths true

#7 Highlight, copy, then paste the following command:
git config --system core.longpaths true

#8 Highlight, copy, then paste the following command:
git config --local core.longpaths true

# 9 - To check to make sure all are set correctly, run each of the lines of code in steps 3 - 9 without the false/true boolean

# Sets autocrlf to false
git config --global core.autocrlf false
git config --system core.autocrlf false
git config --local core.autocrlf false

# Checks to make sure that autocrlf is false
git config --global core.autocrlf
git config --system core.autocrlf
git config --local core.autocrlf

# Sets longpaths to true
git config --global core.longpaths true
git config --system core.longpaths true
git config --local core.longpaths true

# Checks to make sure that longpaths is true
git config --global core.longpaths
git config --system core.longpaths
git config --local core.longpaths