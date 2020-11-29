import {useState} from 'react'
import './Loading.css'
import './firstPage.css'
import {ReactComponent as FaceRecog} from './assets/img/face-recognition-svgrepo-com.svg'


function App() {

  const [link, setLink] = useState('')
  const [verificaRosto, setVerificaRosto] = useState(null)
  const [retornaSentimentos, setRetornaSentimentos] = useState(null)
  const [retornaMediaIdade, setRetornaMediaIdade] = useState(null)
  const [retornaGeneros, setRetornaGeneros] = useState(null)
  const [loading, setLoading] = useState(false)
  const [errorContent, setErrorContent] = useState('')
  const [error, setError] = useState('')

  async function handleOnSubmit(e) {
    e.preventDefault()
    setErrorContent('')
    setError('')
    if(link === ''){
      return setError('Precisa por um Link para enviar!')
    }
    try{
      setLoading(true)
    const responseVerificaRosto = await fetch('http://localhost:5000/FaceRecognition/VerificaRosto', {
      method: 'GET', 
      headers: {
        url: link
      }
    })
    const responseRetornaSentimentos = await fetch('http://localhost:5000/FaceRecognition/RetornaSentimentos', {
      method: 'GET', 
      headers: {
        url: link
      }
    })
    const responseRetornaGeneros = await fetch('http://localhost:5000/FaceRecognition/RetornarGeneros', {
      method: 'GET', 
      headers: {
        url: link
      }
    })
    const responseRetornaMediaIdade = await fetch('http://localhost:5000/FaceRecognition/RetornarMediaIdade', {
      method: 'GET', 
      headers: {
        url: link
      }
    })
    const jsonVerificaRosto = await responseVerificaRosto.json()
    const jsonRetornaSentimentos = await responseRetornaSentimentos.json()
    const jsonRetornaGeneros = await responseRetornaGeneros.json()
    const jsonRetornaMediaIdade = await responseRetornaMediaIdade.json()
    setLoading(false)
    setVerificaRosto(jsonVerificaRosto)
    setRetornaSentimentos(jsonRetornaSentimentos)
    setRetornaMediaIdade(jsonRetornaMediaIdade)
    setRetornaGeneros(jsonRetornaGeneros)
    }catch(err){
      setErrorContent('Imagem não reconhecida!')
      setLoading(false)

    }


  
  }

  function onChange(e){
    setVerificaRosto(null)
    setRetornaSentimentos(null)
    setRetornaMediaIdade(null)
    setRetornaGeneros(null)
    setError('')
    setErrorContent('')
    setLink(e.target.value)
  }

  if(loading){
    return     <div className="wrapper">
    <div className="circle"></div>
    <div className="circle"></div>
    <div className="circle"></div>
    <div className="shadow"></div>
    <div className="shadow"></div>
    <div className="shadow"></div>
    <span>Loading</span>
</div>
  }
  return (
    <div className="grid container">
      <h1>Bem vindo ao site de reconhecimento facial </h1>
      <form className="form-facial" onSubmit={handleOnSubmit}>
        <input type="text" id="link-image" name="link-image" onChange={onChange}/>
        <button type="submit" className="form-facial__button"><FaceRecog/> Faça o reconhecimento facial</button>
      </form>
  {!link && error && <h2 className="error">{error}</h2>}
  {errorContent && <h2 className="error">{errorContent}</h2>}
      {link && <div className="preview-image">
        <img src={link} alt="imagem link"/>
      </div>}
      
      {verificaRosto && retornaSentimentos && retornaMediaIdade && retornaGeneros && 
       <div className="serch-result">
        <div className="have-face">
          <h2>Possui rosto: </h2>
          {verificaRosto.possuiRosto === true ? <span>{verificaRosto.mensagem}</span> : <span>Nao existe rosto</span> }
        </div>
        <div className="have-fellings">
          <h2>Sentimentos: </h2>
          <ul >
          {retornaSentimentos.sentimentos.map((item,index) =>  <li key={index}>{item}</li> )}
          </ul> 
        </div>
        <div className="ages">
          <h2>Media de idade: </h2>
        {retornaMediaIdade && <span>{retornaMediaIdade.mediaIdades}</span>}
        </div>
        <div className="gender">
        <h2>Generos: </h2>
        {retornaGeneros && <ul>
          <li>
            Feminino: {retornaGeneros.generos.Feminino}
          </li>
          <li>
            Masculino: {retornaGeneros.generos.Masculino}
          </li>
          </ul>}
        </div>
        </div>
      }
     
    </div>
  );
}

export default App;
