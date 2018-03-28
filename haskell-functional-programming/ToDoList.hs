main = do
  showHelp
  commandReceiver []

showHelp = putStrLn "\thelp: Show this help message again\n\tcheck: Show todo list\n\tadd <string>: Add new record\n\tdel <index>: Remove specific record"

commandReceiver :: [String] -> IO ()
commandReceiver mylist = do
  cmd <- getLine
  commandParser cmd mylist

commandParser :: String -> [String] -> IO ()
commandParser [] mylist = commandReceiver mylist
commandParser cmd mylist
  | (head $ words cmd) == "help" = do
    showHelp
    commandReceiver mylist
  | (head $ words cmd) == "check" = do
    listChecker $ zip [1..] mylist
    commandReceiver mylist
  | (head $ words cmd) == "add" = do
    putStrLn "Item has been successfully added!"
    commandReceiver $ mylist ++ [unwords (tail $ words cmd)]
  | (head $ words cmd) == "del" = do
    putStrLn "Warning: Item has been successfully removed!"
    commandReceiver $
      removeRecord (zip [1..] mylist) (read $ ((words cmd) !! 1) :: Integer)
  | otherwise = do
    putStrLn "Error: command not implemented yet!"
    commandReceiver mylist

listChecker :: [(Integer, String)] -> IO ()
listChecker [] = return ()
listChecker x = do
  putStrLn $ (show $ fst $ head x) ++ ": " ++ (show $ snd $ head x)
  listChecker $ tail x

removeRecord :: [(Integer, String)] -> Integer -> [String]
removeRecord [] i = []
removeRecord x i
  | (fst $ head x) == i = removeRecord (tail x) i
  | otherwise = [snd $ head x] ++ removeRecord (tail x) i
