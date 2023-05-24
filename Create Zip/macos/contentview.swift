import SwiftUI
import IPWorksZip

struct ContentView: View, ZipDelegate {
    func onBeginFile(index: Int32, skip: inout Bool) {}    
    func onEndFile(index: Int32) {}    
    func onError(description: String, errorCode: Int32, index: Int32, filename: String, ignore: inout Bool) {}    
    func onOverwrite(filename: inout String, overwrite: inout Bool) {}    
    func onPassword(index: Int32, password: inout String) {}    
    func onProgress(data: Data, filename: String, bytesProcessed: Int64, percentProcessed: Int32) {}    
    
    var zip = Zip()
    var documentsPath = NSSearchPathForDirectoriesInDomains(.documentDirectory, .userDomainMask, true)[0] + "/"        
    @State private var zipname: String = ""
    @State private var password: String = ""
    @State private var filename: String = ""
    @State private var outputRes: String = ""
    @State private var connected = false
    
    var body: some View {
      VStack(alignment: .center)
      {
        Text("Enter a file name and press the Create Zip Button to create a zip file. Press Scan Zip to scan the zip file and display the contents.\n")
          .frame(minWidth: 300, maxWidth: 300, alignment: .topLeading).padding(.horizontal, 10)        

        Text("Zip file:")
          .frame(minWidth: 300, maxWidth: 300, alignment: .topLeading).padding(.horizontal, 10)

        TextField("test.zip", text: $zipname)
          .frame(minWidth: 300, maxWidth: 300, alignment: .topLeading).padding(.horizontal, 10).padding(.bottom, 20)

          
        Text("Password:")
          .frame(minWidth: 300, maxWidth: 300, alignment: .topLeading).padding(.horizontal, 10)

        SecureField("enter password...", text: $password)
          .frame(minWidth: 300, maxWidth: 300, alignment: .topLeading).padding(.horizontal, 10).padding(.bottom, 20)

        
        HStack()
        {
          zipButton()            
          scanButton()
        }
        
        Text("Output:")
        TextEditor(text: $outputRes)
          .frame(minWidth: 300, maxWidth: 300, minHeight: 200, maxHeight: 300, alignment: .topLeading)
          .border(Color.black, width: 1)
      }
    }

  @ViewBuilder
  private func zipButton() -> some View {
    Button(action:
      {
        do
        {
          zip.archiveFile = documentsPath+zipname
          try zip.includeFiles(filenames: documentsPath+"Icon.png")
          try zip.compress()                        
          print("Zipped files")
        }
        catch
        {
          print(error)
          return
        }
      })
      {
        Text("Zip Files").font(.system(size: 20))
          .frame(minWidth: 150, minHeight: 40).background(RoundedRectangle(cornerRadius: 8).fill(Color.gray))
      }
      .buttonStyle(PlainButtonStyle())
      .padding(.bottom, 20)
  }

  @ViewBuilder
  private func scanButton() -> some View {
    Button(action:
    {
      outputRes = ""      
      do
      {
        zip.archiveFile = documentsPath + zipname
        try zip.scan()
        print("Scanned zip")
        for zip in zip.files {
            outputRes += zip.decompressedName + "\n"
        }
      }
      catch
      {
        print(error)
        return
      }
    })
    {
      Text("Scan Zip")
        .font(.system(size: 20)).frame(minWidth: 150, minHeight: 40).background(RoundedRectangle(cornerRadius: 8).fill(Color.gray))
    }
    .buttonStyle(PlainButtonStyle())
    .padding(.bottom, 20)
  }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
