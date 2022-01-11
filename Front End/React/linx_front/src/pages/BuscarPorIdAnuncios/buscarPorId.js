import axios from 'axios';
import { Component } from 'react';
import { parseJwt, usuarioAutenticado } from '../../services/auth';

import './buscarPorId.css'
import { Header } from '../../components/Header/Header';


export default class Anuncio extends Component {
  constructor(props) {
    super(props);
    this.state = {
      anuncio: {},
      idAnuncio: window.location.pathname.replace('/anuncio/', ''),
      respostaProposta: ''
    }
  }

  buscarPorId = () => {
    console.log(this.state.idAnuncio)

    axios.get(`http://localhost:5000/api/Anuncio/${this.state.idAnuncio}`)

      .then(resposta => {
        this.setState({ anuncio: resposta.data })
        console.log(this.state.anuncio)
      })

      .catch(erro => console.log(erro))
  }

  cadastrarProposta = (event) => {
    event.preventDefault()

    this.setState({ respostaProposta: "" })

    var body = {
      anuncioId: this.state.idAnuncio
    }

    let headers = {
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
      }
    }

    axios.post(`http://localhost:5000/api/Proposta`, body, headers)

      .then(resposta => {
        console.log(resposta)

        this.setState({ respostaProposta: "Requisição enviada com sucesso" })
      })

      .catch(erro => {
        console.log(erro)
        this.setState({ respostaProposta: "Requisição não pode ser enviada" })
      })

  }

  componentDidMount() {
    this.buscarPorId();
  }

  render() {
    if (usuarioAutenticado() === false) {
      return (
        <>
          <Header />
          <section className='body'>
            <div className='cardAnuncio'>
              <div className='content'>
                <div className='cabecalho'>
                  <h3>{this.state.anuncio.titulo}</h3>
                  <p>De {this.state.anuncio.nomeAutor}</p>
                </div>
                <p className='descricao'>{this.state.anuncio.descricao}</p>
                <div className='areaBotao'></div>
              </div>
            </div>
          </section>
        </>
      )
    }
    else {
      return (
        <>
          <Header />
          <section className='body'>
            <div className='cardAnuncio'>
              <div className='content'>
                <div className='cabecalho'>
                  <h3>{this.state.anuncio.titulo}</h3>
                  <p>De {this.state.anuncio.nomeAutor}</p>
                </div>
                <p className='descricao'>{this.state.anuncio.descricao}</p>
                <div className='espaço'></div>
                <div className='areaBotao'>
                  {this.state.respostaProposta !== '' ? <p className='respostaProposta'>{this.state.respostaProposta}</p> : ''}
                  {parseJwt().role === 'Fornecedor' ? <button className="botaoProposta" onClick={this.cadastrarProposta}>Requisitar Reunião</button> : ''}
                </div>
              </div>
            </div>
          </section>
        </>
      )
    }
  }
}