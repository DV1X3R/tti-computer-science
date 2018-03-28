-- priority of operations
operatorPriority = [ ("+", (+)), ("-", (-)), ("*", (*)), ("/", (/)) ]

calc :: [(String, Double -> Double -> Double)] -> [String] -> Maybe Double
calc [] _ = Nothing   -- operator not found
calc _ [] = Nothing   -- number not found
calc _ [x] = Just (read x)    -- end of the recursion
calc ((strOperator, funcOperator):restOperators) expression =   -- main calc function
  case span (/=strOperator) expression of
    (_, []) -> calc restOperators expression    -- next operator
    (leftSpan, rightSpan) -> funcOperator <$>   -- current operator
      (calc operatorPriority leftSpan) <*> (calc operatorPriority $ drop 1 rightSpan)

main = do
  putStrLn "Enter an expression:"
  x <- getLine
  putStrLn $ show $ calc operatorPriority $ words x
