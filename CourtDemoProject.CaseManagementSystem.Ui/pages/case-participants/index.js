import PropTypes from 'prop-types'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, List, ListItem, ListItemText, Button, IconButton, Link as MuiLink } from '@mui/material'
import DeleteIcon from '@mui/icons-material/Delete'
import NextLink from 'next/link'

const CaseParticipantsPage = ({ caseParticipants }) => {
	const router = useRouter()

	const handleDelete = async (caseParticipantEntityId) => {
		try {
			await axios.delete(`http://api:8080/v1/CaseParticipants/${caseParticipantEntityId}`)
			router.replace(router.asPath) // Refresh the page to update the list
		} catch (error) {
			console.error('Error deleting case participant:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 2 }}>Case Participants</Typography>
			<NextLink href="/case-participants/create" passHref>
				<Button variant="contained" color="primary">Create New Case Participant</Button>
			</NextLink>
			<List>
				{caseParticipants.map((participant) => (
					<ListItem key={participant.caseParticipantEntityId} divider>
						<ListItemText
							primary={`${participant.caseParticipantFirstName} ${participant.caseParticipantMiddleName} ${participant.caseParticipantLastName}`}
							secondary={`Type: ${participant.caseParticipantType}`}
						/>
						<NextLink href={`/case-participants/${participant.caseParticipantEntityId}`} passHref>
							<MuiLink>
								<Button color="primary">View</Button>
							</MuiLink>
						</NextLink>
						<IconButton onClick={() => handleDelete(participant.caseParticipantEntityId)} color="error">
							<DeleteIcon />
						</IconButton>
					</ListItem>
				))}
			</List>
		</Container>
	)
}

export const getServerSideProps = async () => {
	const res = await axios.get('http://api:8080/v1/CaseParticipants')
	const caseParticipants = res.data // Adjust this according to the API response

	return {
		props: { caseParticipants }
	}
}

CaseParticipantsPage.propTypes = {
	caseParticipants: PropTypes.arrayOf(
		PropTypes.shape({
			caseParticipantEntityId: PropTypes.string.isRequired,
			caseParticipantType: PropTypes.number.isRequired,
			caseParticipantFirstName: PropTypes.string.isRequired,
			caseParticipantLastName: PropTypes.string.isRequired,
			caseParticipantMiddleName: PropTypes.string
		})
	).isRequired
}

export default CaseParticipantsPage
