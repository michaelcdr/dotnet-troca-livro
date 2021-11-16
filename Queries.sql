select * from Trocas;

select id, isbn, titulo, Descricao, CadastradoPor, deletado, AlteradoPor, 
DataAlteracao, DataCadastro
from Livros	
order by id desc;

select l.id, l.ISBN, l.Titulo, i.Nome from Imagens i
inner join Livros l on i.LivroId = l.id 
order by l.DataAlteracao desc

select l.id,l.Titulo, l.ISBN from livros l order by l.DataAlteracao

/* INFORMAÇÕES TROCAS */
select t.LivroId,l.titulo,usuariodono.Nome as UsuarioDono, usuarioSolicitante.nome as Solicitante, 
	   t.pontos, 
	   case when t.Status = 0 then 'Disponibilizado'
			when t.Status = 1 then 'TrocaSolicitada'
			when t.Status = 2 then 'TrocaAprovada'
			when t.Status = 3 then 'LivroEnviado'
			when t.Status = 4 then 'LivroRecebido' end as Status, t.Descritivo, t.DataSolicitacaoTroca 
from Trocas t
inner join livros as l on t.LivroId = l.Id
inner join Usuarios as usuarioDono on t.UsuarioQueDisponibilizouParaTrocaId = usuarioDono.Id
inner join Usuarios as usuarioSolicitante on t.UsuarioQueSolicitouTrocaId = usuarioSolicitante.Id ;