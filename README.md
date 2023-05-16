# Present Connection
Simple app that lets users to order pizza with different toppings or pizza sizes. Ordered pizzas can be seen in separate page.
Due to CORDS problem when starting project we need to use chrome without security. this can be done with this script:

#!/bin/sh
user=$(echo $USERNAME)
"C:\Program Files\Google\Chrome\Application\chrome.exe" --disable-web-security --user-data-dir=C:\Users\$user\Desktop\chrometrash
