namespace AInterpreter.Interpreter
{
    class StringHelper
    {
        public string str{get;set;}
        public StringHelper(string str)
        {
            this.str = str;
        }

        public string GetSubstringBetweenIndentifiers(string openSignature, string closingSignature)
        {
            int stringStart       = this.str.IndexOf(openSignature)  + openSignature.Length;
            int stringEnd         = this.str.LastIndexOf(closingSignature) - stringStart;
            string stringToReturn = this.str.Substring(stringStart, stringEnd);
            
            return stringToReturn.Trim();
        }

        public string FromFirstToNextIndentifier(string openSignature, string closingSignature)
        {
            int stringStart       = this.str.IndexOf(openSignature)  + openSignature.Length;
            int stringLenth       = this.str.IndexOf(closingSignature) - closingSignature.Length - stringStart + 1;
            string stringToReturn = this.str.Substring(stringStart, stringLenth);
            
            return stringToReturn.Trim();
        }

        public string GetSubstringFromIndexToSequence(int startIndex, string sequence)
        {
            int stringEnd         = this.str.IndexOf(sequence) - startIndex;
            string stringToReturn = this.str.Substring(startIndex, stringEnd);
            
            return stringToReturn.Trim();
        }

        public string GetSubstringBetweenChars(char openChar, char closingChar)
        {
            int stringStart       = this.str.IndexOf(openChar)  + 1;
            int stringEnd         = this.str.LastIndexOf(closingChar) - stringStart;
            string stringToReturn = this.str.Substring(stringStart, stringEnd);
            
            return stringToReturn.Trim();
        }
        public string RemoveAllExcept(string charactersToKeep)
        {
            string stringToReturn = new string(this.str.Where(c => charactersToKeep.Contains(c)).ToArray());
            string? result = this.str != null ? new string(this.str.Where(c => charactersToKeep.Contains(c)).ToArray()) : null;
            return result == null ? "" : result;
        }
        public string FromStartToNextCharacter(char closingChar)
        {
            int stringStart       = 0;
            int stringEnd         = this.str.IndexOf(closingChar) - stringStart;
            string stringToReturn = this.str.Substring(stringStart, stringEnd);
            
            return stringToReturn;
        }
    
        public string RemoveWhiteSpace()
        {
            int stringEnd         = this.str.Length - 1;
            string betweenindexes = this.str.Substring(0, stringEnd + 1);
            string stringNoSpace  = betweenindexes.Replace(" ", "");
            
            return stringNoSpace;
        }

        public string RemoveWhiteSpace(char openChar, char closingChar)
        {
            int stringStart       = this.str.IndexOf(openChar)  + 1;
            int stringLength      = this.str.LastIndexOf(closingChar) - stringStart;
            string betweenindexes = this.str.Substring(stringStart, stringLength);
            string stringToReturn = betweenindexes.Replace(" ", "");
            
            return stringToReturn;
        }        

        public string RemoveWhiteSpace(int startIndex, int endIndex)
        {
            string betweenindexes = this.str.Substring(startIndex, endIndex);
            string stringToReturn = betweenindexes.Replace(" ", "");
            
            return stringToReturn;
        }

        public string GetSubstring(int startIndex, int endIndex)
        {
            int length = endIndex-startIndex;
            string stringToReturn = this.str.Substring(startIndex, length);
            
            return stringToReturn;
        }
        
        public string GetVariableName()
        {
            string variableName = this.str.TrimStart().Substring(0, str.Length-1);
            
            return variableName;
        }
    }
}