// import React, { Component } from 'react';
import ReactDOM from 'react-dom';

//import './index.css';

import login from './pages/Login/login'
import cadastro from './pages/CadastroUsuario/cadastroUsuario';
import CadastrarAnuncio from './pages/CadastrarAnuncio/cadastrarAnuncio';
import home from './pages/home/home';
import minhasReunioes from './pages/minhasReunioes/minhasReunioes';

import { Route, BrowserRouter as Router, Switch } from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import Anuncio from './pages/BuscarPorIdAnuncios/buscarPorId';
import ReuniaoBuscada from './pages/BuscarPorIdMinhasReunioes/buscarPorIdMinhasReunioes';

// import { parseJwt, usuarioAutenticado } from './services/auth'


const routing = (
  <Router>
    <div>
      <Switch>
        <Route exact path="/minhasReunioes" component={minhasReunioes}/>
        <Route exact path="/login" component={login} /> {/*login*/}
        <Route path="/cadastro" component={cadastro} /> {/* cadastrar usuario */}
        <Route path="/cadastrarAnuncio" component={CadastrarAnuncio}/> {/*cadastrar anuncio */}
        <Route path="/anuncio" component={Anuncio}/> {/*buscar anuncio por id*/}
        <Route path="/minhas" component={ReuniaoBuscada}/>
        <Route path="/" component={home}/> {/*home */}
      </Switch>
    </div>
  </Router>
)



ReactDOM.render(routing, document.getElementById('root'));

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
