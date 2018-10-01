#!/bin/bash

read -p 'Enter Username: ' uvar
read -sp 'Enter Password: ' pvar
pvar=$(echo -n $pvar | shasum -a 256)
echo

echo Hi $uvar, We hashed your password: $pvar
