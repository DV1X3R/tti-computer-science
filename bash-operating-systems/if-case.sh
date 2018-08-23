#!/bin/bash

case $(date +%A) in
    Monday*) echo 'Pls kill me' ;;
    Tuesday*) echo 'Still want to die' ;;
    Wednesday*) echo 'Happy hump day' ;;
    Thursday*) echo 'Its not that bad' ;;
    Friday*) echo 'TGIF' ;;
    Saturday*) echo 'Weekend finally' ;;
    Sunday*) echo 'Ok then' ;;
    *)
        echo idk lol
        ;;
esac
