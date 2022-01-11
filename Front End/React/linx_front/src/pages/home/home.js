import axios from 'axios';
import { Component } from 'react';

import './home.css';
import {Header} from '../../components/Header/Header'
import { CardAnuncio } from '../../components/CardAnuncio/CardAnuncio';

export default class Anuncios extends Component {
  constructor(props) {
    super(props);
    this.state = {
      listarAnuncios: [],
      anuncioId: 'vazio'
    }
  }

  downloadEmployeeData = () => {
    fetch('http://localhost:5000/var/Arquivos/Curriculo.docx')
      .then(response => {
        response.blob().then(blob => {
          let url = window.URL.createObjectURL(blob);
          let a = document.createElement('a');
          a.href = url;
          a.download = 'employees.json';
          a.click();
        });
        window.location.href = response.url;
      });
  }

  buscarAnuncios = () => {
    axios('http://localhost:5000/api/Anuncio', {
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

      .then(resposta => {
        this.setState({ listarAnuncios: resposta.data })
        console.log(this.state.listarAnuncios)
      })

      .catch(erro => console.log(erro))

  }

  buscarId(id) {
    this.props.history.push({
      pathname: `/anuncio/${id}`
    })
  }

  componentDidMount() {
    this.buscarAnuncios();
  }

  render() {
    return (

      <div className="div-home">
        <Header/>
        <div class='centro'>
          <div className='coverDoBanner'>
          <div class='textos'>
            <h2>Anuncia aqui o produto</h2>
            <p>Seja no computador ou no dispositivo móvel, um anúncio exibido no momento certo pode transformar as pessoas em clientes valiosos.</p>
            <a class="cta" href="/cadastro"><button>Junte-se a nós</button></a>
          </div>
          </div>
        </div>
        <section className="form">
          <h1>Anuncios</h1>
          <div className='Cards'>
            {
              this.state.listarAnuncios.map((anuncio) => {
                return (
                  <CardAnuncio anuncio={anuncio} />
                )
              })
            }
          </div>
        </section>
      </div>

    )
  }
}