import PropTypes from 'prop-types'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, List, ListItem, ListItemText, Button, IconButton, Link as MuiLink } from '@mui/material'
import DeleteIcon from '@mui/icons-material/Delete'
import NextLink from 'next/link'

const CasesPage = ({ cases }) => {
	const router = useRouter()

	const handleDelete = async (caseId) => {
		try {
			await axios.delete(`http://api:8080/v1/Cases/${caseId}`)
			router.replace(router.asPath) // Refresh the page to update the list
		} catch (error) {
			console.error('Error deleting case:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 2 }}>Cases</Typography>
			<NextLink href="/cases/create" passHref>
				<Button variant="contained" color="primary">Create New Case</Button>
			</NextLink>
			<List>
				{cases.map((caseItem) => (
					<ListItem key={caseItem.caseId} divider>
						<ListItemText
							primary={`Court Name: ${caseItem.courtName} | Case ID: ${caseItem.caseId}`}
							secondary={`Case Type: ${caseItem.caseType} | Date of Offense: ${caseItem.dateOfOffense} | Status: ${caseItem.caseStatus} | Verdict: ${caseItem.verdict} | Plea: ${caseItem.plea}`}
						/>
						<NextLink href={`/cases/${caseItem.caseId}`} passHref>
							<MuiLink>
								<Button color="primary">View</Button>
							</MuiLink>
						</NextLink>
						<IconButton onClick={() => handleDelete(caseItem.caseId)} color="error">
							<DeleteIcon />
						</IconButton>
					</ListItem>
				))}
			</List>
		</Container>
	)
}

export const getServerSideProps = async () => {
	const res = await axios.get('http://api:8080/v1/Cases')
	const cases = res.data.map(caseItem => ({
		...caseItem,
		caseType: caseItem.caseType,
		caseStatus: caseItem.caseStatus,
		verdict: caseItem.verdict,
		plea: caseItem.plea,
		dateOfOffense: new Date(caseItem.dateOfOffense)
	}))

	return {
		props: { cases }
	}
}

CasesPage.propTypes = {
	cases: PropTypes.arrayOf(
		PropTypes.shape({
			caseId: PropTypes.string.isRequired,
			courtName: PropTypes.string,
			caseType: PropTypes.string,
			dateOfOffense: PropTypes.string,
			caseStatus: PropTypes.string,
			verdict: PropTypes.string,
			plea: PropTypes.string
		})
	).isRequired
}

export default CasesPage
