import axios from 'axios';
import React, { useState } from 'react';
import { Header } from '../../components/Header/Header';

// import { parseJwt } from '../../services/auth';

import './cadastrarAnuncio.css';


export const CadastroAnuncio = (props) => {

  const [titulo, setTitulo] = useState('')
  const [descricao, setDescricao] = useState('')
  const [status, setStatus] = useState(0)

  const cadastrarAnuncio = (event) => {
    event.preventDefault()

    var body = {
      titulo: titulo,
      descricao: descricao,
      status: status
    }

    let headers = {
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
      }
    }

    axios.post('http://localhost:5000/api/Anuncio', body, headers)

      .then(resposta => {
        console.log(resposta)


        if (resposta.data !== null) {
          props.history.push('/');
        }
      })

      .catch(erro => console.log(erro))

  }



  return (
    <>
      <Header />
      <main>
        <section className="cadastro">
          <form className="cadastro-content">
            <div className="titulo">
              <h1>Cadastrar Anuncio</h1>
            </div>
            <div className="form-content">
              <div className="inputs">
                <input
                  type="text"
                  value={titulo}
                  placeholder="Título do Anuncio"
                  onChange={(event) => setTitulo(event.target.value)}
                />
              </div>
              <div className="inputs">
                <textarea
                  type="text"
                  value={descricao}
                  placeholder="Descrição"
                  onChange={(event) => setDescricao(event.target.value)}
                />
              </div>
              <div className="inputs">
                <select
                  value={status}
                  onChange={(event) => setStatus(event.target.value)}>
                  <option>Status do Produto</option>
                  <option value="0">Ativo</option>
                  <option value="1">Inativo</option>
                </select>
              </div>
            </div>
            <div className="box-botao">
              <button onClick={cadastrarAnuncio} type="submit" className="btn-cadastrar">Cadastrar</button>
            </div>
          </form>
        </section>
      </main>
    </>
  )
}


export default CadastroAnuncio;