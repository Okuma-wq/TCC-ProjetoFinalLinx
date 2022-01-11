import React from 'react'
import { useHistory } from 'react-router-dom'
import Logo from '../../assets/imagens/logo.png'
import { parseJwt, usuarioAutenticado } from '../../services/auth'

import './HeaderStyle.css'

export const Header = (props) => {

    const history = useHistory();

    const Logout = () => {
        localStorage.removeItem('usuario-login')
        history.push({ pathname: '/' })
    }

    if (usuarioAutenticado() === false)
        return (
            <>
                <header className='header'>
                    <div className='header-content'>
                        <a onClick={() => history.push({ pathname: '/' })} className="logo"><img src={Logo}  /></a>
                        <div>
                            <button className="btn" onClick={() => history.push({ pathname: '/login' })}>Entrar</button>
                        </div>
                    </div>
                </header>
            </>
        )
    else
        return (
            <>


                {parseJwt().role === 'Lojista' ?
                    <header className='header'>
                        <div className='header-content'>
                            <a onClick={() => history.push({ pathname: '/' })} className="logo"><img src={Logo}  /></a>
                            <div className="areaLinks">
                                <a className='linksHeader' onClick={() => history.push({ pathname: `/cadastrarAnuncio` }) }>Cadastrar Anuncio</a>
                                <a className='linksHeader' onClick={() => history.push({ pathname: `/minhasReunioes` })}>Minhas Reuniões</a>
                            </div>
                            <div>
                                <button className="btn" onClick={() => Logout()}>Sair</button>
                            </div>
                        </div>
                    </header>
                    : ''}

                {parseJwt().role === 'Fornecedor' ?
                    <header className='header'>
                        <div className='header-content'>
                            <a onClick={() => history.push({ pathname: '/' })} className="logo"><img src={Logo} /></a>
                            <div className="btn-home">
                                <a className='linksHeader' onClick={() => history.push({ pathname: `/minhasReunioes` })}>Minhas Reuniões</a>
                            </div>
                            <div>
                                <button className="btn" onClick={() => Logout()}>Sair</button>
                            </div>
                        </div>
                    </header>
                    : ''}
            </>
        )
}
