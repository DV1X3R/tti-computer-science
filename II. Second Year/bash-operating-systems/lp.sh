#!/bin/bash

odd_even () {
    echo $([ $(($i % 2)) -eq 0 ] && echo even || echo odd)
}

files_count () {
    echo [dir $(ls -1q $1 | wc -l) files]
}

file_size () {
    echo [$(wc -c $1 | awk '{ print $1 }') bytes]
}

for i in {1..10}
do echo $i $(odd_even $i)
done

echo

for i in $(ls $1)
do
    if [ -d $1/$i ]
    then echo $i $(files_count $1/$i)
    else echo $i $(file_size $1/$i)
    fi
done
